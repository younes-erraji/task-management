import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import axios from 'axios';
import { SignalRService } from '../../Services/signal-r.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
})
export class TasksComponent {
  tasks: any[] = [];
  tasksExecution: any[] = [];

  tableName: string = '';
  actionType: string = '';
  name: string = '';

  constructor(public signalRService: SignalRService) {
    this.getTasks();
    this.getTasksExecution();
  }

  getTasks() {
    axios
      .get(`${environment.root}/Tasks`)
      .then(({ data }) => {
        this.tasks = [];
        data.forEach((task: any) => {
          this.tasks.push(task);
        });
      })
      .catch((error: any) => console.log(error));
  }

  getTasksExecution() {
    axios
      .get(`${environment.root}/TasksExecution`)
      .then(({ data }) => {
        this.tasksExecution = [];
        data.forEach((task: any) => {
          this.tasksExecution.push(task);
        });
      })
      .catch((error: any) => console.log(error));
  }

  addTask(e: any) {
    axios
      .post(`${environment.root}/Tasks/insert`, {
        tableName: this.tableName,
        actionType: this.actionType,
      })
      .then((response: any) => response.data)
      .then((data: any) => {
        Swal.fire('Task', 'Task was Successfully Inserted.', 'success');

        this.getTasks();
      });
  }

  executeTask(id: string) {
    axios
      .post(`${environment.root}/TasksExecution/${id}/execute`)
      .then(({ data }) => {
        // Fire TaskStarted
        this.signalRService.invokeStartingTask(data.taskId);
        this.getTasksExecution();
        axios
          .post(`${environment.root}/TasksExecution/${data.id}/complete`)
          .then((response: any) => {
            // Fire TaskEnd
            this.signalRService.invokeEndingTask(response.data.taskId);
            Swal.fire(
              `A Task On "${response.data.task.tableName}" Has Been Completed!`,
              response.data.task.actionType,
              'success'
            );
            this.getTasksExecution();
          })
          .catch(console.log);
      })
      .catch((error: any) => console.log(error));
  }

  refreshName() {
    this.name = `${this.actionType} data from the ${this.tableName} table`;
  }
}
