import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TerminalComponent } from './terminal.component';
import { FormsModule } from '@angular/forms';
import {FlexLayoutModule} from '@angular/flex-layout'


@NgModule({
  declarations: [
    TerminalComponent
  ],
  exports: [
    TerminalComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    FlexLayoutModule
  ]
})
export class TerminalModule { }
