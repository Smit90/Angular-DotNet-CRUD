import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environment/environment';
import { Employee } from '../employee/employee.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  baseApiUrl: string = environment.baseApiUrl
  constructor(private http: HttpClient) { }

  getAllEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.baseApiUrl + '/api/employees')
  }

  addEmployee(employeeRequest: Employee): Observable<Employee> {
    employeeRequest.id = '00000000-0000-0000-0000-000000000000'
    return this.http.post<Employee>(this.baseApiUrl + '/api/employees', employeeRequest)
  }

  getEmployee(id: string): Observable<Employee> {
    return this.http.get<Employee>(this.baseApiUrl + '/api/employees/' + id)
  }

  updateEmployee(id: string, updateEmployeeRequest: Employee) {
    return this.http.put<Employee>(this.baseApiUrl + '/api/employees/' + id, updateEmployeeRequest)
  }

  deleteEmployee(id: string): Observable<string> {
    return this.http.delete<string>(this.baseApiUrl + '/api/employees/' + id)
  }

}
