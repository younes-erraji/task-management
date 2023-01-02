import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import axios from 'axios';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html'
})
export class TasksComponent {
  tasks: any[] = [];
  tasksExecution: any[] = [];

  tableName:string = '';
  actionType:string = '';
  name:string = '';

  constructor() {
    this.getTasks();
    this.getTasksExecution();
  }

  getTasks() {
    axios.get('https://localhost:7240/api/Tasks').then(({data}) => {
      this.tasks = [];
      data.forEach((task:any) => {
        this.tasks.push(task);
      });
    }).catch((error:any) => console.log(error))
  }

  getTasksExecution() {
    axios.get('https://localhost:7240/api/TasksExecution').then(({data}) => {
      this.tasksExecution = [];
      console.log(data)
      data.forEach((task:any) => {

        this.tasksExecution.push(task);
      });
    }).catch((error:any) => console.log(error))
  }

  addTask(e: any) {
    axios.post('https://localhost:7240/api/Tasks/insert', {
      tableName: this.tableName,
      actionType: this.actionType
    }).then((response:any) => response.data).then((data:any) => {

      Swal.fire(
        'Task',
        'Task was Successfully Inserted.',
        'success'
      );

      this.getTasks();
    })
  }

  executeTask(id:string) {
    axios.post(`https://localhost:7240/api/TasksExecution/${id}/execute`)
      .then(({data}) => {
        this.getTasksExecution();

        setInterval(() => {
          this.getTasksExecution();
        }, 1000)
      })
      .catch(error => console.log(error));
  }
}
