import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { ClansState } from "store/root/organization/clan/types";
import {
  loadClans,
  downloadClanLogo,
  uploadClanLogo,
  loadClan,
  createClan
} from "store/actions/clan";
import { RootState } from "store/types";
import produce, { Draft } from "immer";
import { UserId } from "types/identity";
import { ClanId } from "types/clans";

interface StateProps {
  clans: ClansState;
}

interface OwnProps {
  userId: UserId;
  clanId: ClanId;
}

export const withClans = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect(() => {
      props.actions.loadClans();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
    state,
    ownProps
  ) => {
    const clans = produce(
      state.root.organization.clan,
      (draft: Draft<ClansState>) => {
        draft.data.forEach(clan => {
          clan.owner = {
            userId: clan.ownerId
          };
        });
      }
    );
    return {
      clans,
      userId: ownProps.userId,
      userClan: clans.data.find(
        clan =>
          clan.id === ownProps.clanId ||
          clan.members.some(member => member.userId === ownProps.userId)
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
