import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import axios from 'axios';
import { SignalRService } from 'src/app/Services/signal-r.service';
import { environment } from 'src/environments/environment';

interface Student {
  id: string;
  name: string;
  birthDate: string;
  yearOfStudy: number;
}

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html'
})
export class StudentsComponent implements OnInit {
  students: Array<Student> = [];

  student: Student = {
    id: '',
    name: '',
    birthDate: '',
    yearOfStudy: 0,
  }

  studentToUpdate: Student = {
    id: '',
    name: '',
    birthDate: '',
    yearOfStudy: 0,
  }

  selectedStudent: string = '';

  constructor(public signalRService: SignalRService) {
    this.getStudents();

    this.signalRService.onStudentsTaskStarted((action:string) => {
      Swal.fire(
        `A Task On Students Table Has Been Started!`,
        action,
        'success'
      );

      this.getStudents();
    });
    this.signalRService.onStudentsTaskEnd((action:string) => {
      Swal.fire(
        `A Task On Students Has Been Completed!`,
        action,
        'success'
      );

      this.getStudents();
    });
  }

  ngOnInit(): void {
  }

  getStudents() {
    axios.get(`${environment.root}/Students`).then(({data}) => {
      this.students = [];
      data.forEach((s:Student) => {
        this.students.push(s)
      });
    }).catch((error:any) => console.log(error))
  }

  emptyStudentObject(): Student {
    return {
      id: '',
      name: '',
      birthDate: '',
      yearOfStudy: 0,
    }
  }

  addStudent(e: any) {
    axios.post(`${environment.root}/Students/insert`, {
      ...this.student
    }).then((response:any) => response.data).then((data:any) => {

      this.student = this.emptyStudentObject();
      Swal.fire(
        'Student was Successfully Inserted.',
        '',
        'success'
      );

      this.getStudents();
    })
  }

  updateStudent(e: any) {
    axios.put(`${environment.root}/Students/${this.studentToUpdate.id}/edit`, {
      ...this.studentToUpdate
    }).then(({data}) => {
      Swal.fire(
        'Student was Successfully UPDATED!',
        '',
        'warning'
      );

      this.getStudents();
    }).catch(error => console.log(error))
  }

  deleteStudent(id: string) {

    Swal.fire({
      title: 'Sure you want to DELETE this Student?',
      showCancelButton: true,
      confirmButtonText: 'Save',
      denyButtonText: `Don't save`,
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        axios.delete(`${environment.root}/Students/${id}/delete`)
          .then(({data}) => {
            Swal.fire(
              'Student was Successfully DELETED!',
              '',
              'warning'
            );

            this.getStudents();
          })
          .catch(error => console.log(error))
      }
    })
  }

  getStudent(id: string):Student|any {
    axios.get(`${environment.root}/Students/${id}/get`)
    .then(({data}) => {
      this.studentToUpdate = data
    }).catch(error => console.log(error))
  }
}
