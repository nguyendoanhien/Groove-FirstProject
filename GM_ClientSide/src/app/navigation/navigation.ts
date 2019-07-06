import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id       : 'applications',
        title    : 'Applications',
        translate: 'NAV.APPLICATIONS',
        type     : 'group',
        children : [
            {
                id       : 'chat',
                title    : 'Chat',               
                type     : 'item',
                icon     : 'chat',
                url      : 'apps/chat',
                badge    : {
                    title: '13',
                    bg   : '#09d261',
                    fg   : '#FFFFFF'
                }
            }
        ]
    }
];
