import { Injectable } from '@angular/core'
import { Action, Selector, State, StateContext, StateToken } from '@ngxs/store'
import { User } from '@shared/models'
import { RoomUsersActions } from '@features/chat/store/actions'
import { append, patch, removeItem } from '@ngxs/store/operators'
import { v4 as uuidv4 } from 'uuid'

export const ROOM_STATE_TOKEN = new StateToken<RoomStateModel>('room')

export interface RoomStateModel {
  users: User[]
}

@State({
  name: ROOM_STATE_TOKEN,
  defaults: {
    users: [],
  },
})
@Injectable()
export class RoomState {
  @Selector([ROOM_STATE_TOKEN])
  static users(state: RoomStateModel): User[] {
    return state.users
  }

  @Action(RoomUsersActions.Add)
  addUser(
    ctx: StateContext<RoomStateModel>,
    { userToAdd }: RoomUsersActions.Add
  ) {
    const id = uuidv4()
    userToAdd.id = id

    ctx.setState(patch({ users: append([userToAdd]) }))
  }

  @Action(RoomUsersActions.Remove)
  removeUser(
    ctx: StateContext<RoomStateModel>,
    { userToRemove }: RoomUsersActions.Remove
  ) {
    ctx.setState(
      patch({ users: removeItem<User>((user) => user.id === userToRemove.id) })
    )
  }
}
