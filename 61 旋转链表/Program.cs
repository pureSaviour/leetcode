int[] arr = [1, 2, 3, 4, 5];
ListNode head = new ListNode(arr[0]);
int index = 1;
for(ListNode temp = head; index < arr.Length; ++index,temp = temp.next)
    temp.next = new ListNode(arr[index]);


Solution sol = new();
var res = sol.RotateRight(head, 2);
for(ListNode temp = res; temp is not null; temp = temp.next)
    Console.Write($"{temp.val} ");


public class Solution {
    public ListNode RotateRight(ListNode head, int k)
    {
        if (head is null || k == 0) return head!;
        int count = 0;
        ListNode tail = head;
        while(tail is not null)
        {
            count++;
            if(tail.next is null)
                break;
            tail = tail.next;
        }
        k %= count;
        if(k == 0)
            return head;
        ListNode newTail = head;
        for (int i = 0; i < count - k - 1; ++i)
            newTail = newTail.next!;

        var newHead = newTail.next;
        newTail.next = null;
        tail?.next = head;
        return newHead!;
    }
}

public class ListNode(int val = 0, ListNode? next = null)
{
    public int val = val;
    public ListNode? next = next;
}