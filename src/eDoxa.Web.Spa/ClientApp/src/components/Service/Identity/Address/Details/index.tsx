import React, { FunctionComponent } from "react";
import { compose } from "recompose";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { Address } from "types/identity";

type OwnProps = {
  address: Address;
};

type StateProps = {
  countryName: string;
};

type InnerProps = StateProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const Details: FunctionComponent<Props> = ({ countryName, address }) => (
  <address className="text-uppercase m-0">
    <span>{address.line1}</span>
    <br />
    {address.line2 && <span>{address.line2}</span>}
    {address.line2 && <br />}
    <span>{address.city}</span>
    {address.state && <span>{`, ${address.state}`}</span>}
    {address.postalCode && <span>{` ${address.postalCode}`}</span>}
    <br />
    <span>{countryName}</span>
  </address>
);

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const country = state.static.identity.countries.find(
    country => country.isoCode === ownProps.address.countryIsoCode
  );
  return {
    countryName: country.name
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(Details);
