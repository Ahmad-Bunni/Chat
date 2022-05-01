import { CommonModule } from '@angular/common'
import { NgModule } from '@angular/core'
import { NgxsModule } from '@ngxs/store'
import { SignInComponent, SignUpComponent } from '.'
import { LoginRoutingModule } from './login.routing'
import { SignInState } from './store/states'

@NgModule({
  declarations: [SignInComponent, SignUpComponent],
  imports: [
    CommonModule,
    LoginRoutingModule,
    NgxsModule.forFeature([SignInState]),
  ],
})
export class LoginModule {}
