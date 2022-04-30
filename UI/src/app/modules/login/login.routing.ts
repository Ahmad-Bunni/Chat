import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { AuthService } from 'src/app/core/services/auth.service'
import { LoginComponent } from '.'
import { SignUpComponent } from './containers/sign-up/sign-up.component'

const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
  },
  {
    path: 'sign-up',
    component: SignUpComponent,
  },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [AuthService],
})
export class LoginRoutingModule {}
