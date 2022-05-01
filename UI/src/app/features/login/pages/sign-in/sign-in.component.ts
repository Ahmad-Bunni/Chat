import { Component } from '@angular/core'
import { AuthenticationService } from '@core/services'

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss'],
})
export class SignInComponent {
  username: string

  constructor(private authService: AuthenticationService) {}

  login() {
    this.authService.requestToken(this.username) // TODO use ngxs or ngrx dispatch action
  }
}
