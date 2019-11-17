import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import {
  loadClans,
  loadClan,
  createClan
} from "store/root/organization/clan/actions";
import { ClansState } from "store/root/organization/clan/types";
import {
  downloadClanLogo,
  uploadClanLogo
} from "store/root/organization/logo/actions";
import { RootState } from "store/types";
import produce, { Draft } from "immer";
import { UserId, ClanId } from "types";

export const withClans = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect(() => {
      props.actions.loadClans();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    const clans: ClansState = produce(
      state.root.organization.clan,
      (draft: Draft<ClansState>) => {
        draft.data.forEach(clan => {
          clan.owner = {
            userId: clan.ownerId
          };
        });
      }
    );
    const userId: UserId = state.oidc.user.profile["sub"];
    const clanId: ClanId = state.oidc.user.profile["clanId"];
    return {
      clans,
      userId,
      userClan: clans.data.find(
        clan =>
          clan.id === clanId ||
          clan.members.some(member => member.userId === userId)
      )
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadClans: () => dispatch(loadClans()),
        loadClan: (clanId: string) => dispatch(loadClan(clanId)),
        addClan: (data: any) => dispatch(createClan(data)).then(loadClans()),
        loadLogo: (clanId: string) => dispatch(downloadClanLogo(clanId)),
        updateLogo: (clanId: string, data: any) =>
          dispatch(uploadClanLogo(clanId, data))
      }
    };
  };

  return connect(mapStateToProps, mapDispatchToProps)(Container);
};
