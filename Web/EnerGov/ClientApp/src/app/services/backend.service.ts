import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class BackendService {

  url = 'https://localhost:44328/Employee/'
  
  constructor(private http:HttpClient,private _snackBar: MatSnackBar) { }


  createEmployee(emp:any){
    return this.http.post(`${this.url}CreateEmployee`,emp)
  }

  getEmployeeFromManager(managerName:string){
    return this.http.get(`${this.url}GetEmployeesByManagerId?managerId=${managerName}`)
  }

  getAllEmployees(){
    return this.http.get(`${this.url}GetAllEmployees`)
  }
  
  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }
}
