import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

  public username: string;

  constructor(private authService: AuthService ) { }

  ngOnInit() {
  }

  async login() {

    await this.authService.requestToken(this.username)

  }
}
