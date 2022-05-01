import { Injectable } from '@angular/core'
import { State, StateToken } from '@ngxs/store'
import { User } from '@shared/models'
import { Authentication } from '@shared/models/authentication.model'

const SIGNIN_STATE_TOKEN = new StateToken<SignInStateModel>('signIn')

export class SignInStateModel {
  user: User
  authentication: Authentication
}

@State({
  name: SIGNIN_STATE_TOKEN,
  defaults: {
    user: undefined,
    authentication: undefined,
  },
})
@Injectable()
export class SignInState {}
