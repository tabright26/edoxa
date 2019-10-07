import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadInvitationsWithClanId, loadInvitationsWithUserId, loadInvitation, addInvitation, acceptInvitation, declineInvitation } from "store/organizations/invitations/actions";
import { AppState } from "store/types";

export const connectInvitations = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, invitations, ...attributes }) => {
    return <ConnectedComponent actions={actions} invitations={invitations} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      userId: state.oidc.user.profile.sub,
      invitations: state.organizations.invitations
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadInvitationsWithClanId: (clanId: string) => dispatch(loadInvitationsWithClanId(clanId)),
        loadInvitationsWithUserId: (userId: string) => dispatch(loadInvitationsWithUserId(userId)),
        addInvitation: (data: any) => dispatch(addInvitation(data)),
        loadInvitation: (invitationId: string) => dispatch(loadInvitation(invitationId)),
        acceptInvitation: (invitationId: string) => dispatch(acceptInvitation(invitationId)),
        declineInvitation: (invitationId: string) => dispatch(declineInvitation(invitationId))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
