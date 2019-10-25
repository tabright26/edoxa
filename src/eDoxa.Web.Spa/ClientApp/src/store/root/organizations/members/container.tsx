import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadClanMembers, kickClanMember } from "store/root/organizations/members/actions";
import { ClanMembersState } from "store/root/organizations/members/types";
import { leaveClan } from "store/root/organizations/clans/actions";
import { RootState } from "store/types";
import produce, { Draft } from "immer";

export const withClanMembers = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, members, clanId, ...attributes }) => {
    useEffect(() => {
      actions.loadMembers(clanId);
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [clanId]);
    return <HighOrderComponent actions={actions} members={members} clanId={clanId} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    const members = produce(state.root.organizations.members, (draft: Draft<ClanMembersState>) => {
      draft.data.forEach(member => {
        member.doxatag = state.root.doxatags.data.find(doxatag => doxatag.userId === member.userId) || null;
      });
    });
    return {
      members
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadMembers: (clanId: string) => dispatch(loadClanMembers(clanId)),
        kickMember: (clanId: string, memberId: string) => dispatch(kickClanMember(clanId, memberId)).then(dispatch(loadClanMembers(clanId))),
        leaveClan: (clanId: string) => dispatch(leaveClan(clanId))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
