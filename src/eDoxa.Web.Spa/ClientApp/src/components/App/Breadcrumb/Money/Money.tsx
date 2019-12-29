import React, { FunctionComponent, useState } from "react";
import { Badge, Popover, PopoverBody, PopoverHeader } from "reactstrap";
import Balance from "components/User/Account/Balance";
import DepositMoney from "./Deposit";
import WithdrawalMoney from "./Withdrawal";
import { CURRENCY_MONEY } from "types";

const MoneyBreadcrumb: FunctionComponent<any> = ({ className }) => {
  const [open, setOpen] = useState(false);
  return (
    <div className={className}>
      <Badge
        id="moneyPopover"
        className="bg-gray-900"
        style={{ width: "100px" }}
      >
        <Balance currency={CURRENCY_MONEY} attribute="available" />
      </Badge>
      <Popover
        style={{
          width: "250px"
        }}
        placement="bottom"
        isOpen={open}
        target="moneyPopover"
        trigger="hover"
        delay={{ show: 0, hide: 250 }}
        toggle={() => setOpen(!open)}
      >
        <PopoverHeader>MONEY</PopoverHeader>
        <PopoverBody>
          <dl className="row mb-0">
            <dt className="col-6">Available</dt>
            <dd className="col-6">
              <Balance
                currency={CURRENCY_MONEY}
                attribute="available"
                alignment="right"
              />
            </dd>
            <dt className="col-6">Pending</dt>
            <dd className="col-6">
              <Balance currency={CURRENCY_MONEY} attribute="pending" alignment="right" />
            </dd>
          </dl>
          {process.env.NODE_ENV !== "production" && (
            <DepositMoney currency={CURRENCY_MONEY} />
          )}
          {process.env.NODE_ENV !== "production" && (
            <WithdrawalMoney currency={CURRENCY_MONEY} />
          )}
        </PopoverBody>
      </Popover>
    </div>
  );
};

export default MoneyBreadcrumb;
