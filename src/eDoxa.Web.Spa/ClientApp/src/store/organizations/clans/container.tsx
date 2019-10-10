import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadClans, loadClan, addClan, loadLogo, updateLogo } from "store/organizations/clans/actions";
import { AppState } from "store/types";
import { show } from "redux-modal";

import { CREATE_CLAN_MODAL } from "modals";

export const connectClans = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, clans, userId, userClan, ...attributes }) => {
    useEffect(() => {
      actions.loadClans();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} clans={clans} userId={userId} userClan={userClan} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    const clans = state.organizations.clans.map(clan => {
      const doxaTag = state.doxaTags.find(doxaTag => doxaTag.userId === clan.ownerId);

      clan.ownerDoxaTag = doxaTag ? doxaTag.name + "#" + doxaTag.code : null;
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
        addClan: (data: any) => dispatch(addClan(data)).then(loadClans()),
        showCreateAddressModal: () => dispatch(show(CREATE_CLAN_MODAL)),
        loadLogo: (clanId: string) => dispatch(loadLogo(clanId)),
        updateLogo: (clanId: string, data: any) => dispatch(updateLogo(clanId, data))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
