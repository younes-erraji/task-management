import { Component, OnInit } from '@angular/core';

interface Student {
  id: string;
  name: string;
  birthDate: string;
  yearOfStudy: number;
}

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
})
export class StudentsComponent implements OnInit {
  students: Array<Student> = [
    {
      id: 'arti',
      name: 'Younes',
      birthDate: 'date',
      yearOfStudy: 187,
    },
    {
      id: 'arti4',
      name: 'Younes',
      birthDate: 'date',
      yearOfStudy: 187,
    },
    {
      id: 'arti7',
      name: 'Younes',
      birthDate: 'date',
      yearOfStudy: 187,
    },
  ];

  id: string = '';
  name: string = '';
  birthDate: string = '';
  yearOfStudy!: number;

  constructor() {}

  ngOnInit(): void {}

  track(index: number, student: Student) {
    return student.id;
  }

  parseStringToInt(value: string) {
    return parseInt(value);
  }

  addStudent(e: any) {
    console.log(e);
    // e.preventDefault();

    console.log(this.name);
    console.log(this.birthDate);
    console.log(this.yearOfStudy);
  }

  updateStudent(e: any) {
    console.log(e);
  }

  deleteStudent(e: any) {
    console.log(e);
  }
}
