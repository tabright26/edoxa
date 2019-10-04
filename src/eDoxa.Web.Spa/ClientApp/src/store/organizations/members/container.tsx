import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadMembers, kickMember, leaveClan } from "store/organizations/members/actions";
import { AppState } from "store/types";

export const connectMembers = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, members, ...attributes }) => {
    return <ConnectedComponent actions={actions} members={members} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
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
