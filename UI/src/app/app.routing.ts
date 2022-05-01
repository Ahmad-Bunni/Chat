import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'

const routes: Routes = [
  {
    path: '',
    // TOOD canload -> login
    loadChildren: () =>
      import('./features/chat/chat.module').then((mod) => mod.ChatModule),
  },
  {
    path: 'login',

    loadChildren: () =>
      import('./features/login/login.module').then((mod) => mod.LoginModule),
  },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
