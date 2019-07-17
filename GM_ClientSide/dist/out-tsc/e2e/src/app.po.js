import { browser, by, element } from 'protractor';
export class FusePage {
    navigateTo() {
        return browser.get('/');
    }
    getParagraphText() {
        return element(by.css('app #main')).getText();
    }
}
//# sourceMappingURL=app.po.js.map