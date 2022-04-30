import { CommonModule } from '@angular/common'
import { NgModule } from '@angular/core'
import { HomeComponent } from '.'
import { HomeRoutingModule } from './home.routing'

@NgModule({
  declarations: [HomeComponent],
  imports: [CommonModule, HomeRoutingModule],
})
export class HomeModule {}
