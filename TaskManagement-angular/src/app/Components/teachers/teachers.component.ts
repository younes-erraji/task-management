import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import axios from 'axios';
import { SignalRService } from 'src/app/Services/signal-r.service';
import { environment } from 'src/environments/environment';

interface Teacher {
  id: string;
  name: string;
  birthDate: string;
  mainSubjectTeaching: string;
}

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html'
})
export class TeachersComponent {
  teachers: Array<Teacher> = [];

  teacher: Teacher = {
    id: '',
    name: '',
    birthDate: '',
    mainSubjectTeaching: '',
  }

  teacherToUpdate: Teacher = {
    id: '',
    name: '',
    birthDate: '',
    mainSubjectTeaching: '',
  }

  selectedTeacher: string = '';

  constructor(public signalRService: SignalRService) {
    this.getTeachers();

    this.signalRService.onTeachersTaskStarted((action:string) => {
      Swal.fire(
        `A Task On Teachers Has Been Started!`,
        action,
        'success'
      );

      this.getTeachers();
    });
    this.signalRService.onTeachersTaskEnd((action:string) => {
      Swal.fire(
        `A Task On Teachers Has Been Completed!`,
        action,
        'success'
      );

      this.getTeachers();
    });
  }

  getTeachers() {
    axios.get(`${environment.root}/Teachers`).then(({data}) => {
      this.teachers = [];
      data.forEach((s:Teacher) => {
        this.teachers.push(s)
      });
    }).catch((error:any) => console.log(error))
  }

  emptyTeacherObject(): Teacher {
    return {
      id: '',
      name: '',
      birthDate: '',
      mainSubjectTeaching: '',
    }
  }

  addTeacher(e: any) {
    axios.post(`${environment.root}/Teachers/insert`, {
      ...this.teacher
    }).then((response:any) => response.data).then((data:any) => {

      this.teacher = this.emptyTeacherObject();
      Swal.fire(
        'Teacher',
        'Teacher was Successfully Inserted.',
        'success'
      );

      this.getTeachers();
    })
  }

  updateTeacher(e: any) {
    axios.put(`${environment.root}/Teachers/${this.teacherToUpdate.id}/edit`, {
      ...this.teacherToUpdate
    }).then(({data}) => {
      Swal.fire(
        'Teacher was Successfully Updated!',
        '',
        'warning'
      );

      this.getTeachers();
    }).catch(error => console.log(error))
  }

  deleteTeacher(id: string) {

    Swal.fire({
      title: 'Sure you want to DELETE this Teacher?',
      showCancelButton: true,
      confirmButtonText: 'Save',
      denyButtonText: `Don't save`,
    }).then((result) => {
      if (result.isConfirmed) {
        axios.delete(`${environment.root}/Teachers/${id}/delete`)
          .then(({data}) => {
            Swal.fire(
              'Teacher was Successfully DELETED!',
              '',
              'warning'
            );

            this.getTeachers();
          })
          .catch(error => console.log(error))
      }
    })
  }

  getTeacher(id: string):Teacher|any {
    axios.get(`${environment.root}/Teachers/${id}/get`)
    .then(({data}) => {
      console.log(data)
      this.teacherToUpdate = data
    }).catch(error => console.log(error))
  }

}
