import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Employee } from 'src/app/employee/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

  employeeDetails: Employee = {
    id: '',
    name: '',
    email: '',
    phone: 0,
    salary: 0,
    department: ''
  }

  constructor(private employeeService: EmployeesService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id')
        if (id) {
          this.employeeService.getEmployee(id).subscribe({
            next: (employee) => {
              this.employeeDetails = employee
            },
            error: (err) => {
              console.log("errrrrrrrr", err)
            }
          })
        }
      }
    })
  }

  updateEmployee() {
    this.employeeService.updateEmployee(this.employeeDetails.id, this.employeeDetails).subscribe({
      next: (updatedEmployee) => {
        console.log("updatedEmployee", updatedEmployee)
        this.router.navigate(['employees'])
      },
      error: (err) => {
        console.log("err", err)
      }
    })
  }

  deleteEmployee(id: string) {
    this.employeeService.deleteEmployee(id).subscribe({
      next: (res) => {
        console.log("res", res)
        this.router.navigate(['employees'])
      },
      error: (err) => {
        console.log("err", err)
      }
    })
  }
}
