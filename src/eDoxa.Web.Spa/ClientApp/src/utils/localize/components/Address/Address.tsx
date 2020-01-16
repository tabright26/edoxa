import React, { FunctionComponent } from "react";
import { compose } from "recompose";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { UserAddress, CountryOptions } from "types";

interface OwnProps {
  address: UserAddress;
}

interface StateProps {
  countries: CountryOptions[];
}

type InnerProps = StateProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const Address: FunctionComponent<Props> = ({ countries, address }) => {
  const Country = React.lazy(() =>
    import(`./${address.country.toString().toUpperCase()}`)
  );
  return (
    <Country
      name={
        countries.find(
          country =>
            country.twoIso.toUpperCase() ===
            address.country.toString().toUpperCase()
        ).name
      }
      {...address}
    />
  );
};

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  return {
    countries: state.static.identity.data.addressBook.countries
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(Address);
