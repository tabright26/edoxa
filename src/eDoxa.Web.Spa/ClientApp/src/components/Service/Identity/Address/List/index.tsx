import React, { FunctionComponent } from "react";
import { compose } from "recompose";
import { Loading } from "components/Shared/Loading";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import Item from "components/Service/Identity/Address/List/Item";
import { Address } from "types/identity";

type OwnProps = {};

type StateProps = {
  addresses: Address[];
  loading: boolean;
};

type InnerProps = StateProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const List: FunctionComponent<Props> = ({ addresses, loading }) =>
  loading ? (
    <Loading />
  ) : (
    <>
      {addresses.map((address, index) => (
        <Item
          key={index}
          address={address}
          hasMore={addresses.length !== index + 1}
        />
      ))}
    </>
  );

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  return {
    addresses: state.root.user.addressBook.data,
    loading: state.root.user.addressBook.loading
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(List);
