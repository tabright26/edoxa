import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadClans, loadClan, createClan } from "store/root/organizations/clans/actions";
import { downloadClanLogo, uploadClanLogo } from "store/root/organizations/logos/actions";
import { RootState } from "store/root/types";
import { show } from "redux-modal";

import { CREATE_CLAN_MODAL } from "modals";

export const withClans = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, clans, userId, userClan, ...attributes }) => {
    useEffect(() => {
      actions.loadClans();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent actions={actions} clans={clans} userId={userId} userClan={userClan} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    const clans = state.organizations.clans.data.map(clan => {
      const doxatag = state.doxatags.data.find(doxatag => doxatag.userId === clan.ownerId);

      clan.ownerDoxatag = doxatag ? doxatag.name + "#" + doxatag.code : null;
      return clan;
    });

    const userId = state.oidc.user.profile.sub;

    return {
      clans,
      userId,
      userClan: clans.find(clan => clan.members.find(member => member.userId === userId))
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadClans: () => dispatch(loadClans()),
        loadClan: (clanId: string) => dispatch(loadClan(clanId)),
        addClan: (data: any) => dispatch(createClan(data)).then(loadClans()),
        showCreateAddressModal: () => dispatch(show(CREATE_CLAN_MODAL)),
        loadLogo: (clanId: string) => dispatch(downloadClanLogo(clanId)),
        updateLogo: (clanId: string, data: any) => dispatch(uploadClanLogo(clanId, data))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
