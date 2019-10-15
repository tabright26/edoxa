import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadInvitations, loadInvitation, addInvitation, acceptInvitation, declineInvitation } from "store/root/organizations/invitations/actions";
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
      const doxatag = state.doxatags.data.find(doxaTag => doxaTag.userId === invitation.userId);
      const clan = state.organizations.clans.data.find(clan => clan.id === invitation.clanId);

      invitation.userDoxaTag = doxatag ? doxatag.name + "#" + doxatag.code : null;
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
        loadInvitations: () => dispatch(loadInvitations(ownProps.type, ownProps.id)),
        loadInvitation: (invitationId: string) => dispatch(loadInvitation(invitationId)),
        acceptInvitation: (invitationId: string) => dispatch(acceptInvitation(invitationId)).then(loadInvitations(ownProps.type, ownProps.id)),
        declineInvitation: (invitationId: string) => dispatch(declineInvitation(invitationId)).then(loadInvitations(ownProps.type, ownProps.id)),
        addInvitation: (clanId: string, userId: string) => dispatch(addInvitation(clanId, userId)).then(loadInvitations(ownProps.type, ownProps.id))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
