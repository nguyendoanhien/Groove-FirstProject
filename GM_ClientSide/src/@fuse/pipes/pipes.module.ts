import { NgModule } from "@angular/core";

import { KeysPipe } from "./keys.pipe";
import { GetByIdPipe } from "./getById.pipe";
import { HtmlToPlaintextPipe } from "./htmlToPlaintext.pipe";
import { FilterPipe } from "./filter.pipe";
import { CamelCaseToDashPipe } from "./camelCaseToDash.pipe";
import { UnknownContactFilterPipe } from "app/custom-pipe/unknown-contact-filter.pipe";
import { ExtractUrlPipe } from "./extract-url-pipe";
import { DetectUrlPipe } from "./detect-url.pipe";
import { FilterUnknownContactPipe } from './filter-unknown-contact.pipe';
import { filterGroupPipe } from './filter-group.pipe';

@NgModule({
    declarations: [
        KeysPipe,
        GetByIdPipe,
        HtmlToPlaintextPipe,
        FilterPipe,
        CamelCaseToDashPipe,
        UnknownContactFilterPipe,
        ExtractUrlPipe,
        DetectUrlPipe,
        FilterUnknownContactPipe,
        filterGroupPipe
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
        DetectUrlPipe,
        FilterUnknownContactPipe,
        filterGroupPipe
    ]
})
export class FusePipesModule {
}