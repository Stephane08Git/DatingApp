import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.scss']
})
export class TestErrorsComponent implements OnInit {
  baseUrl = environment.apiUrl;
  validationErrors: string[];
  constructor(private http: HttpClient) { 
    this.validationErrors = [];
  }

  ngOnInit(): void {
  }

  get404Error() {
    this.http.get(`${this.baseUrl}buggy/not-found`).subscribe(response => {
      console.log('response :>> ', response);
    }, error => {
      console.log('error :>> ', error);
    })
  }

  get400Error() {
    this.http.get(`${this.baseUrl}buggy/bad-request`).subscribe(response => {
      console.log('response :>> ', response);
    }, error => {
      console.log('error :>> ', error);
    })
  }

  get500Error() {
    this.http.get(`${this.baseUrl}buggy/server-error`).subscribe(response => {
      console.log('response :>> ', response);
    }, error => {
      console.log('error :>> ', error);
    })
  }

    get401Error() {
    this.http.get(`${this.baseUrl}buggy/auth`).subscribe(response => {
      console.log('response :>> ', response);
    }, error => {
      console.log('error :>> ', error);
    })
  }

  get400ValidationError() {
    this.http.post(`${this.baseUrl}account/register`, {}, {
      observe : 'response'
    }).subscribe(response => {
      console.log('response :>> ', response);
    }, error => {
      console.log('error :>> ', error);
      this.validationErrors = error;
    })
  }
}
