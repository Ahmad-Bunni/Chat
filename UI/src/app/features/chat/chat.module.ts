import { CommonModule } from '@angular/common'
import { NgModule } from '@angular/core'
import { NgxsModule } from '@ngxs/store'
import { RoomComponent } from '.'
import { ChatRoutingModule } from './chat.routing'
import { RoomState } from './store/states'

@NgModule({
  declarations: [RoomComponent],
  imports: [
    CommonModule,
    ChatRoutingModule,
    NgxsModule.forFeature([RoomState]),
  ],
})
export class ChatModule {}
