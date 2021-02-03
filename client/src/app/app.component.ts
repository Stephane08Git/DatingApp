import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title: string = 'App Dating';
  users: any;

  constructor(private httpClient: HttpClient) {

  }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    return this.httpClient.get('https://localhost:5001/api/users').subscribe(
      (response) => {
        this.users = response;
      },
      (error) => {
        console.log(error);
      },
      () => {

      }
    );
  }
}
