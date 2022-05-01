import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { RoomComponent } from '.'

const routes: Routes = [
  {
    path: '',
    // TODO can activate
    component: RoomComponent,
  },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ChatRoutingModule {}
