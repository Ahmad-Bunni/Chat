import { CommonModule } from '@angular/common'
import { NgModule } from '@angular/core'
import { LoginComponent } from '.'
import { SignUpComponent } from './containers/sign-up/sign-up.component'
import { LoginRoutingModule } from './login.routing'

@NgModule({
  declarations: [LoginComponent, SignUpComponent],
  imports: [CommonModule, LoginRoutingModule],
})
export class LoginModule {}
