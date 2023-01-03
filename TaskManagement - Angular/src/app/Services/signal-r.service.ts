import { Injectable } from '@angular/core';
import * as SignalR from '@microsoft/signalr'

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  connection: SignalR.HubConnection;
  constructor() {
    this.connection = new SignalR.HubConnectionBuilder()
    .withUrl("https://localhost:7240/notification", {
      skipNegotiation: true,
      transport: SignalR.HttpTransportType.WebSockets,
    })
    .build();


    this.connection
      .start()
      .then(() => {
        console.log("Connection started!");
      })
      .catch((error:any) => console.log(error));
  }


  invokeStartingTask(taskId:string) {
    this.connection
    .invoke("StartingTask", taskId)
    .catch(console.log);
  }

  invokeEndingTask(taskId:string) {
    this.connection
    .invoke("EndingTask", taskId)
    .catch(console.log);
  }

  onTeachersTaskStarted(callback:Function) {
    this.connection.on("TeachersTaskStarted", function (action:string) {
      callback(action);
    });
  }

  onStudentsTaskStarted(callback:Function) {
    this.connection.on("StudentsTaskStarted", function (action:string) {
      callback(action);
    });
  }

  onStudentsTaskEnd(callback:Function) {
    this.connection.on("StudentsTaskEnd", function (action:string) {
      callback(action);
    });
  }

  onTeachersTaskEnd(callback:Function) {
    this.connection.on("TeachersTaskEnd", function (action:string) {
      callback(action);
    });
  }
}
