import { Component, OnInit } from '@angular/core';

interface Teacher {
  id: string;
  name: string;
  birthDate: string;
  mainSubjectTeaching: string;
}

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
})
export class TeachersComponent implements OnInit {
  teachers: Array<Teacher> = [
    {
      id: 'arti',
      name: 'Younes',
      birthDate: 'date',
      mainSubjectTeaching: 'Math 1',
    },
    {
      id: 'arti4',
      name: 'Younes',
      birthDate: 'date',
      mainSubjectTeaching: 'Math 3',
    },
    {
      id: 'arti7',
      name: 'Younes',
      birthDate: 'date',
      mainSubjectTeaching: 'Math 7',
    },
  ];

  id: string = '';
  name: string = '';
  birthDate: string = '';
  mainSubjectTeaching: string = '';

  constructor() {}

  ngOnInit(): void {}

  track(index: number, teacher: Teacher) {
    return teacher.id;
  }

  addTeacher(e: any) {
    console.log(e);
    // e.preventDefault();

    console.log(this.name);
    console.log(this.birthDate);
    console.log(this.mainSubjectTeaching);
  }

  updateTeacher(e: any) {
    console.log(e);
  }

  deleteTeacher(e: any) {
    console.log(e);
  }
}
