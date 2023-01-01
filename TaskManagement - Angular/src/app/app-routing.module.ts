import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TasksComponent } from './Components/tasks/tasks.component';
import { StudentsComponent } from './Components/students/students.component';
import { TeachersComponent } from './Components/teachers/teachers.component';

const routes: Routes = [
  {
    path: '',
    component: TasksComponent,
  },
  {
    path: 'tasks',
    component: TasksComponent,
  },
  {
    path: 'students',
    component: StudentsComponent,
  },
  {
    path: 'teachers',
    component: TeachersComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
