//
// Copyright Fela Ameghino 2015-2025
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
using Telegram.Td.Api;
using Windows.System;

namespace Telegram.ViewModels.Delegates
{
    public interface IGroupCallDelegate : IViewModelDelegate
    {
        void UpdateGroupCallParticipant(GroupCallParticipant participant);

        void VideoInfoAdded(GroupCallParticipant participant, params GroupCallParticipantVideoInfo[] videoInfos);
        void VideoInfoRemoved(GroupCallParticipant participant, params string[] endpointIds);

        DispatcherQueue DispatcherQueue { get; }
    }
}
