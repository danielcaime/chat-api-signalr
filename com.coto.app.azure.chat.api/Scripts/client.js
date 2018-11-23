; (function () {
    'use strict';

    var oldMessages = Array();
    // env values
    var customerName = 'pepe';
    var customerId = '26363216';
    // Declare a proxy to reference the hub.
    $.connection.hub.url = "http://localhost:50014/signalr";
    var chat = $.connection.chatHub;
    // Create a function that the hub can call to broadcast messages.
    chat.client.broadcastMessage = function (name, message) {
        $('div .content').show();
        $('div .form-chat').show();

        console.log(name,message);
        // Html encode display name and message.
        var encodedName = $('<div />').text(name).html();

        if (message.Type === 'text') {
            if ($.inArray(message.Id, oldMessages) == -1) {
                oldMessages.push(message.Id);

                if (name == customerName) {
                    addCustomerMessage(message.Text);
                } else {
                    addServerMessage(name, message.Text);
                }
                //var encodedMsg = $('<div />').text(message.Text).html();
            }
            var height = $('div .content')[0].scrollHeight;
            $('div .content').scrollTop(height);
        }
        else if (message.Type === 'case') {
            $('#casesUl').append('<li><strong>Caso</strong>:&nbsp;&nbsp;<small data-id="' + message.RefId + '">' + message.Ref + '</small></li>');
        }
        else {
            var encodedMsg = '<div><img src="' + message.ImageHeaders + message.ImageBinary + '" ></div>';

            $('#discussion').append('<li><strong>' + encodedName
                + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
        }
        //$('#discussion').append(image);
    };

    let addCustomerMessage = (text) => {

        var input = `<div class="right">
            <div class="author-name">
                yo
                    <small class="chat-date">
                    11:24 am
                    </small>
            </div>
            <div class="chat-message">
                ${text}
                </div>
        </div>`;
        $('div .content').append(input);
    }

    let addServerMessage = (name, text) => {
        var input = ` <div class="left">
                <div class="author-name">
                    ${name}<small class="chat-date">
                    10:02 am
                </small>
                </div>
                <div class="chat-message active">
                    ${text}
                </div>
            </div>`;
        $('div .content').append(input);
    }

    var getCases = (subsId, callback, refId = '') => {

        var api = 'https://cotoapp-lab-appservice.azurewebsites.net/api/LinkCaseQry';
        var apiDesa = 'http://localhost:62888/api/LinkCaseQry';

        let lcqf = {
            SubscriptionId: subsId,
            FromDate: '01/01/2018',
            RefId: refId,
            CustomerId: '26363216',
        }

        $.ajax({
            url: api,
            method: "POST",
            data: lcqf,
            success: function (response) {
                console.log(response);
                callback(response.Data)
            },
            error: function (request, status, errorThrown) {
                console.log(request)
            }
        });
    }

    // Start the connection.
    $.connection.hub.start().done(function () {

        chat.server.register(customerId, 'BCEDC838-A6EB-4154-B8E0-8CEE6991F0C5', customerName);

        //chat.server.joinRoom('otro');

        $(document).on('click', '#send', function (e) {
            //console.log('send');
            // Call the Send method on the hub.
            chat.server.send($('#user').val(), $('#text').val());
            // Clear text box and reset focus for next comment.
            $('#text').val('').focus();
        });

        $('#sendimage').click(function () {
            chat.server.sendImage($('#displayname').val(), $('#message').val());
        });

        $(document).on('touch click', 'small', function (e) {
            console.log($(this).data('id'));
            chat.server.subscribe($(this).data('id'));
        });

        $(document).on('touch click', 'small', function (e) {
            console.log($(this).data('id'));
            //console.log($(this).text());
            //chat.server.userRegister(UserId, channel.Id, 'Juan', SubscriptionId, $(this).data('id'));
            getCases(SubscriptionId, fillMessages, $(this).data('id'))
        });


    })
        .fail(function (err) { console.log('Could not connect' + err); });


    $(function () {
        //addServerMessage('juan', 'buen dia')
    });
}());