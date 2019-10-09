import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { loadInvitationsWithClanId, loadInvitationsWithUserId, loadInvitation, addInvitation, acceptInvitation, declineInvitation } from "store/organizations/invitations/actions";
import { AppState } from "store/types";

export const connectInvitations = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, invitations, userId, ...attributes }) => {
    return <ConnectedComponent actions={actions} invitations={invitations} userId={userId} {...attributes} />;
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
        loadInvitationsWithUserId: (clanId: string) => dispatch(loadInvitationsWithUserId(clanId)),
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
