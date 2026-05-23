using WishList.Domain.Base;
using WishList.Domain.Enums;
using WishList.Domain.Exceptions;
using WishList.ValueObjects;

namespace WishList.Domain.Entities;

public class User : Entity<Guid>
{
    private readonly ICollection<Gift> _gifts = new List<Gift>();

    public UserId UserId { get; private set; }
    public Username Username { get; private set; }
    public IReadOnlyCollection<Gift> Gifts => _gifts as IReadOnlyCollection<Gift> ?? _gifts.ToList().AsReadOnly();

    public User(UserId userId, Username username) : base(Guid.NewGuid())
    {
        UserId = userId ?? throw new ArgumentNullValueException(nameof(userId));
        Username = username ?? throw new ArgumentNullValueException(nameof(username));
    }

    protected User() : base() { }

    public Gift CreateGift(Title title, Link? link, Price price)
    {
        var gift = new Gift(this, title, link, price);
        _gifts.Add(gift);
        return gift;
    }

    public bool EditGift(Gift gift, Title? title, Link? link, Price? price)
    {
        if (gift.User != this)
            throw new AnotherUserEditGiftException(gift, this);

        if (!_gifts.Contains(gift))
            throw new GiftNotBelongUserException(gift, this);

        return gift.Update(title, link, price);
    }

    public bool DeleteGift(Gift gift)
    {
        if (gift.User != this)
            throw new AnotherUserDeleteGiftException(gift, this);

        if (!_gifts.Contains(gift))
            throw new GiftNotBelongUserException(gift, this);

        return _gifts.Remove(gift);
    }

    public bool MarkGiftAsPurchased(Gift gift)
    {
        if (gift.User != this)
            throw new AnotherUserEditGiftException(gift, this);

        if (!_gifts.Contains(gift))
            throw new GiftNotBelongUserException(gift, this);

        return gift.MarkAsPurchasedByOwner();
    }

}