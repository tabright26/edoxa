import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadMembers, kickMember } from "store/root/organizations/members/actions";
import { leaveClan } from "store/root/organizations/clans/actions";
import { RootState } from "store/root/types";

export const withClanMembers = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, members, clanId, ...attributes }) => {
    useEffect(() => {
      actions.loadMembers(clanId);
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [clanId]);
    return <HighOrderComponent actions={actions} members={members} clanId={clanId} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    const members = state.organizations.members.data.map(member => {
      const doxaTag = state.doxatags.data.find(doxaTag => doxaTag.userId === member.userId);

      member.userDoxaTag = doxaTag ? doxaTag.name + "#" + doxaTag.code : null;
      return member;
    });

    return {
      members
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadMembers: (clanId: string) => dispatch(loadMembers(clanId)),
        kickMember: (clanId: string, memberId: string) => dispatch(kickMember(clanId, memberId)).then(dispatch(loadMembers(clanId))),
        leaveClan: (clanId: string) => dispatch(leaveClan(clanId))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
