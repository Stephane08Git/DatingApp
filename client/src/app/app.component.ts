import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { PresenceService } from './_services/presence.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title: string = 'App Dating';

  constructor(private accountService: AccountService, private presence: PresenceService) {

  }

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    let user: User | null = null;

    if (user) {
      this.accountService.setCurrentUser(user);
      this.presence.createHubConnection(user);
    }
    if (user) this.accountService.setCurrentUser(user);
  }

}
