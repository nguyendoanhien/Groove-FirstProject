import { NgModule } from "@angular/core";

import { KeysPipe } from "./keys.pipe";
import { GetByIdPipe } from "./getById.pipe";
import { HtmlToPlaintextPipe } from "./htmlToPlaintext.pipe";
import { FilterPipe } from "./filter.pipe";
import { CamelCaseToDashPipe } from "./camelCaseToDash.pipe";
import { UnknownContactFilterPipe } from "app/custom-pipe/unknown-contact-filter.pipe";
import { ExtractUrlPipe } from "./extract-url-pipe";
import { DetectUrlPipe } from "./detect-url.pipe";

@NgModule({
    declarations: [
        KeysPipe,
        GetByIdPipe,
        HtmlToPlaintextPipe,
        FilterPipe,
        CamelCaseToDashPipe,
        UnknownContactFilterPipe,
        ExtractUrlPipe,
        DetectUrlPipe
    ],
    imports: [],
    exports: [
        KeysPipe,
        GetByIdPipe,
        HtmlToPlaintextPipe,
        FilterPipe,
        CamelCaseToDashPipe,
        UnknownContactFilterPipe,
        ExtractUrlPipe,
        DetectUrlPipe
    ]
})
export class FusePipesModule {
}