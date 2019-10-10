import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { loadMembers, kickMember, leaveClan } from "store/organizations/members/actions";
import { RootState } from "store/types";

export const connectMembers = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, members, userId, ...attributes }) => {
    return <ConnectedComponent actions={actions} members={members} userId={userId} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      userId: state.oidc.user.profile.sub,
      members: state.organizations.members
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadMembers: (clanId: string) => dispatch(loadMembers(clanId)),
        kickMember: (clanId: string, memberId: string) => dispatch(kickMember(clanId, memberId)),
        leaveClan: (clanId: string) => dispatch(leaveClan(clanId))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
