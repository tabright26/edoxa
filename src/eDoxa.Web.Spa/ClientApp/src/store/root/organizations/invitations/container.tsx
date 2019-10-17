import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadClanInvitations, loadClanInvitation, sendClanInvitation, acceptClanInvitation, declineClanInvitation } from "store/root/organizations/invitations/actions";
import { RootState } from "store/root/types";

interface InvitationProps {
  type: string;
  id: string;
}

export const withInvitations = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, invitations, ...attributes }) => {
    useEffect(() => {
      actions.loadInvitations();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent actions={actions} invitations={invitations} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    const invitations = state.organizations.invitations.data.map(invitation => {
      const doxatag = state.doxatags.data.find(doxatag => doxatag.userId === invitation.userId);
      const clan = state.organizations.clans.data.find(clan => clan.id === invitation.clanId);

      invitation.userDoxatag = doxatag ? doxatag.name + "#" + doxatag.code : null;
      invitation.clanName = clan ? clan.name : null;
      return invitation;
    });

    return {
      invitations
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: InvitationProps) => {
    return {
      actions: {
        loadInvitations: () => dispatch(loadClanInvitations(ownProps.type, ownProps.id)),
        loadInvitation: (invitationId: string) => dispatch(loadClanInvitation(invitationId)),
        acceptInvitation: (invitationId: string) => dispatch(acceptClanInvitation(invitationId)).then(loadClanInvitations(ownProps.type, ownProps.id)),
        declineInvitation: (invitationId: string) => dispatch(declineClanInvitation(invitationId)).then(loadClanInvitations(ownProps.type, ownProps.id)),
        addInvitation: (clanId: string, userId: string) => dispatch(sendClanInvitation(clanId, userId)).then(loadClanInvitations(ownProps.type, ownProps.id))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
