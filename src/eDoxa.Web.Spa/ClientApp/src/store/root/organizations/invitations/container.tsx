import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { loadClanInvitations, loadClanInvitation, sendClanInvitation, acceptClanInvitation, declineClanInvitation } from "store/root/organizations/invitations/actions";
import { ClanInvitationsState } from "store/root/organizations/invitations/types";
import { RootState } from "store/types";
import produce, { Draft } from "immer";

interface StateProps {
  invitations: ClanInvitationsState;
}

interface OwnProps {
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

  const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = state => {
    return {
      invitations: produce(state.root.organizations.invitations, (draft: Draft<ClanInvitationsState>) => {
        draft.data.forEach(invitation => {
          invitation.doxatag = state.root.doxatags.data.find(doxatag => doxatag.userId === invitation.userId) || null;
          invitation.clan = state.root.organizations.clans.data.find(clan => clan.id === invitation.clanId) || null;
        });
      })
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
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
