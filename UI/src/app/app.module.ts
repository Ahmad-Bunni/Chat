import { BrowserModule } from '@angular/platform-browser'
import { NgModule } from '@angular/core'
import { AppComponent } from './app.component'
import { AppRoutingModule } from './app.routing'
import { LoginModule } from './modules/login/login.module'
import { HttpClientModule } from '@angular/common/http'

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, HttpClientModule, AppRoutingModule, LoginModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
