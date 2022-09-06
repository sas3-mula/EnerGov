import { Component, OnInit } from '@angular/core';
import { BackendService } from 'src/app/services/backend.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  managerList :any = []
  employeeList :any = []
  displayedColumns:string[]= []
  id:string=''

  constructor(private backendService:BackendService) { }

  ngOnInit(): void {
    this.displayedColumns = ['id','firstName','lastName']
    this.employeeList=[]
    this.backendService.getAllEmployees().subscribe((i:any)=>{
      let data =i.table
      this.managerList = data.map((i:any)=>i.id)
      
    })
  }

  getEmployees(id:any){
    this.id=id
    this.backendService.getEmployeeFromManager(id).subscribe((i:any)=>{
      let data =i.table
      this.employeeList =data.map((i:any)=>i)
    })
  }

}
