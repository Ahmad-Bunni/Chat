import { User } from '@shared/models'

export namespace RoomUsersActions {
  export class Add {
    static readonly type = '[Chat Users] AddChatUser'
    constructor(public userToAdd: User) {}
  }

  export class Remove {
    static readonly type = '[Chat Users] RemoveChatUser'
    constructor(public userToRemove: User) {}
  }
}
