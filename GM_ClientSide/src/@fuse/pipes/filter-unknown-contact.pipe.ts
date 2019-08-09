import { Pipe, PipeTransform } from "@angular/core";
import { FuseUtils } from "@fuse/utils";
import { ChatService } from 'app/apps/chat/chat.service';
import { from, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

@Pipe({ name: "filterUnknownContact" })
export class FilterUnknownContactPipe implements PipeTransform {
    /**
     * Transform
     *
     * @param {any[]} mainArr
     * @param {string} searchText
     * @param {string} property
     * @returns {any}
     */


    constructor(private _chatService: ChatService) {

    }
    subject: Subject<string>;
    transform(unknownContacts: any[], searchText: string, property: string): any {
        
        return from(this._chatService.getUnknownContacts(searchText)).pipe(
            map(data => {
                if (!unknownContacts.equals(data)) return data;
                else return unknownContacts;
            })
        );

    }
}