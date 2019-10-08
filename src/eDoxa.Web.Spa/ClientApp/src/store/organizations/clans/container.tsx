import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadClans, loadClan, addClan } from "store/organizations/clans/actions";
import { AppState } from "store/types";
import { show } from "redux-modal";

import { CREATE_CLAN_MODAL } from "modals";

export const connectClans = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, clans, userId, ...attributes }) => {
    return <ConnectedComponent actions={actions} clans={clans} userId={userId} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      userId: state.oidc.user.profile.sub,
      clans: state.organizations.clans
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadClans: () => dispatch(loadClans()),
        loadClan: (clanId: string) => dispatch(loadClan(clanId)),
        addClan: (data: any) => dispatch(addClan(data)),
        showCreateAddressModal: () => dispatch(show(CREATE_CLAN_MODAL))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};