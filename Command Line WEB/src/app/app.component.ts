import { Component } from '@angular/core';
import { TerminalPrompt } from './components/terminal/terminal-prompt';
import { TerminalService } from './shared/service/terminal.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public server = 'server:8080';
  public login = 'User';

  constructor(private terminalService: TerminalService){}

  onCommand(prompt: TerminalPrompt) {
    // exemplo de pegar os valores digitados no terminal.
    // console.log(prompt.getCommand())
    // console.log(prompt.getArgs())
    // console.log(prompt.getCommandAndArgs())

    switch (prompt.getCommand()) {

      case 'connect':
        switch(prompt.getArgs()[0]){
          case 'PC1':
            // Enviando parametros para o servidor.
            this.terminalService.sendCommand(prompt.getCommandAndArgs()).then(resp => {
              prompt.appendAnsiResponse('[\u001b[0;31m  Falha  \u001b[0m] PC1 está offline');
            }).catch(err => {
              prompt.appendAnsiResponse('[\u001b[0;31m  Falha  \u001b[0m] PC1 está offline');
            }).finally(() => {
              prompt.appendAnsiResponse('');
            });
            break;
          case 'PC2':
            prompt.appendAnsiResponse('[\u001b[0;31m  Falha  \u001b[0m] PC2 está offline');
            break;
          case 'PC3':
            prompt.appendAnsiResponse('[\u001b[0;32m  Connectado  \u001b[0m] conectado no PC3');
            break;
           default:
              prompt.response = `${prompt.getArgs()[0]} não foi registrado`;
        }
        prompt.responseComplete();
        break;

      case 'list':
        prompt.appendAnsiResponse('[\u001b[0;31m  Disabled  \u001b[0m] PC1');
        prompt.appendAnsiResponse('[\u001b[0;31m  Disabled  \u001b[0m] PC2');
        prompt.appendAnsiResponse('[\u001b[0;32m  Enabled   \u001b[0m] PC3');
        prompt.responseComplete();
        break;

      default:
        prompt.response = 'unknown command';
        prompt.responseComplete();
    }
  }
}
