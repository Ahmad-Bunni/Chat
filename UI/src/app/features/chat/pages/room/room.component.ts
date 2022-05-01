import { Component } from '@angular/core'
import { User } from '@app/shared/models'
import { Select, Store } from '@ngxs/store'
import { Observable } from 'rxjs'
import { RoomUsersActions } from '@features/chat/store/actions'
import { RoomState, RoomStateModel } from '@features/chat/store/states'

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss'],
})
export class RoomComponent {
  @Select(RoomState) room$: Observable<RoomStateModel>

  constructor(private store: Store) {}

  addUser(user: User) {
    if (!user) return

    this.store.dispatch(new RoomUsersActions.Add(user))
  }

  removeUser(user: User) {
    if (!user) return

    this.store.dispatch(new RoomUsersActions.Remove(user))
  }
}
