import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title: string = 'App Dating';

  constructor(private accountService: AccountService) {

  }

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    let user: User | null = null;

    if (localStorage.getItem('user') !== null) {
      user = JSON.parse(<string>localStorage.getItem('user'));
    }
    this.accountService.setCurrentUser(user);
  }

}
