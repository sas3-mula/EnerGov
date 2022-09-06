import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BackendService } from 'src/app/services/backend.service';
import { AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  model: any = {};
  managerList: any = []
  employeeForm!: FormGroup
  invalidEmpId:boolean=false
  

  constructor(private formBuilder: FormBuilder, private backendService: BackendService) { }

  ngOnInit(): void {

    this.backendService.getAllEmployees().subscribe((i: any) => {
      let data = i.table
      this.managerList = data.map((i: any) => i.id)

    })

    this.employeeForm = this.formBuilder.group({
      managerId: ['', Validators.required],
      employeeId: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      roles: ['', Validators.required],
    })
  }

  ValidateEmpId() {
    let id = this.employeeForm.get('employeeId')?.value
    var emp = this.managerList.filter((i: any) => i == id)
    if (emp.length>0) {
      this.invalidEmpId=true
    }
    else{
      this.invalidEmpId = false
    }
  }

  addEmployee() {
    let dataObj = this.employeeForm.value
    if(this.validate(dataObj)){
      this.backendService.createEmployee(dataObj).subscribe(i=>{this.employeeForm.reset()
         this.backendService.openSnackBar('Employee Added','Success')})
    }
  }

  validate(data:any){
    if (data.firstName=='' || data.firstName==null){
      this.backendService.openSnackBar('invalid firstname','Error')
      return false
    }
    else if (data.lastName=='' || data.lastName==null){
      this.backendService.openSnackBar('invalid lastName','Error')
      return false
    }
    else if (data.roles=='' || data.roles==null){
      this.backendService.openSnackBar('invalid roles','Error')
      return false
    }
    else if (data.employeeId=='' || data.employeeId==null || this.invalidEmpId){
      this.backendService.openSnackBar('Invalid Employee Id','Error')
      return false
    }
    else{
      return true
    }
  }
}


